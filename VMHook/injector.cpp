#include <windows.h>
#include <tlhelp32.h>
#include <iostream>
#include <fstream>
#include <string>
#include <vector>

std::string trim(const std::string& str) {
    size_t start = str.find_first_not_of(" \t\r\n");
    size_t end = str.find_last_not_of(" \t\r\n");
    return (start == std::string::npos) ? "" : str.substr(start, end - start + 1);
}

std::wstring GetVMPathFromEnv() {
    std::ifstream envFile(".env");
    std::string line;
    while (std::getline(envFile, line)) {
        line = trim(line);
        if (line.find("VMPath=") == 0) {
            std::string path = line.substr(7);
            return std::wstring(path.begin(), path.end());
        }
    }
    return L"";
}

std::string GetVMPasswordFromEnv() {
    std::ifstream envFile(".env");
    std::string line;
    while (std::getline(envFile, line)) {
        line = trim(line);
        if (line.find("VMPassword=") == 0) {
            return line.substr(11); // after "VMPassword="
        }
    }
    return "";
}

DWORD StartVMPlayerAndGetPID(const std::wstring& vmxPath) {
    STARTUPINFOW si = { sizeof(si) };
    PROCESS_INFORMATION pi;
    std::wstring command = L"\"C:\\Program Files (x86)\\VMware\\VMware Player\\vmplayer.exe\" \"" + vmxPath + L"\"";
    if (CreateProcessW(NULL, &command[0], NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi)) {
        CloseHandle(pi.hThread);
        return pi.dwProcessId;
    }
    return 0;
}

void SendPasswordToVMware(const std::string& password) {
    Sleep(3000); // wait for VMware to show the GUI
    HWND hwnd = FindWindow(NULL, L"VMware Workstation 17 Player (Non-commercial use only)");
    if (!hwnd) {
        MessageBoxA(NULL, "❌ VMware window not found!", "Injector", MB_OK | MB_ICONERROR);
        return;
    }

    SetForegroundWindow(hwnd);
    Sleep(1000); // wait for focus

    std::vector<INPUT> inputs;
    for (char ch : password) {
        INPUT input = {};
        input.type = INPUT_KEYBOARD;
        input.ki.wVk = 0;
        input.ki.wScan = ch;
        input.ki.dwFlags = KEYEVENTF_UNICODE;
        inputs.push_back(input);

        INPUT up = input;
        up.ki.dwFlags |= KEYEVENTF_KEYUP;
        inputs.push_back(up);
    }

    // Press ENTER
    INPUT enterDown = { INPUT_KEYBOARD };
    enterDown.ki.wVk = VK_RETURN;
    INPUT enterUp = enterDown;
    enterUp.ki.dwFlags = KEYEVENTF_KEYUP;
    inputs.push_back(enterDown);
    inputs.push_back(enterUp);

    SendInput((UINT)inputs.size(), inputs.data(), sizeof(INPUT));
}

int main() {
    std::wstring vmxPath = GetVMPathFromEnv();
    std::string password = GetVMPasswordFromEnv();

    if (vmxPath.empty()) {
        MessageBoxA(NULL, "❌ VMPath not found in .env!", "Injector Error", MB_OK | MB_ICONERROR);
        return 1;
    }

    if (password.empty()) {
        MessageBoxA(NULL, "❌ VMPassword not found in .env!", "Injector Error", MB_OK | MB_ICONERROR);
        return 1;
    }

    DWORD pid = StartVMPlayerAndGetPID(vmxPath);
    if (pid == 0) {
        MessageBoxA(NULL, "❌ Failed to start VMware Player.", "Injector Error", MB_OK | MB_ICONERROR);
        return 1;
    }

    Sleep(2000); // wait for vmplayer UI to fully load
    SendPasswordToVMware(password);

    MessageBoxA(NULL, ("✅ Password injected! " + password).c_str(), "Injector", MB_OK | MB_ICONINFORMATION);

    return 0;
}
