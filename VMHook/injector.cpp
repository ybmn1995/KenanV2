#include <windows.h>
#include <tlhelp32.h>
#include <iostream>
#include <fstream>
#include <string>

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
            return line.substr(11);
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

// Recursively search for Edit control that follows Static with label "P&assword:"
HWND FindTargetEditControl(HWND parent, bool& passwordLabelFound) {
    HWND child = NULL;
    wchar_t className[256];
    wchar_t windowText[256];

    while ((child = FindWindowExW(parent, child, NULL, NULL)) != NULL) {
        GetClassNameW(child, className, 256);
        GetWindowTextW(child, windowText, 256);

        if (wcscmp(className, L"Static") == 0 && wcscmp(windowText, L"P&assword:") == 0) {
            passwordLabelFound = true;
        }
        else if (passwordLabelFound && wcscmp(className, L"Edit") == 0) {
            return child;
        }

        // Dive into nested children
        HWND nested = FindTargetEditControl(child, passwordLabelFound);
        if (nested != NULL) return nested;
    }
    return NULL;
}

void InjectPasswordDirect(const std::string& password) {
    HWND vmwareMain = FindWindow(NULL, L"VMware Workstation 17 Player (Non-commercial use only)");
    if (!vmwareMain) {
        MessageBoxA(NULL, "❌ VMware window not found.", "Injector", MB_OK | MB_ICONERROR);
        return;
    }

    bool passwordLabelFound = false;
    HWND passwordEdit = FindTargetEditControl(vmwareMain, passwordLabelFound);

    if (!passwordEdit) {
        MessageBoxA(NULL, "❌ Password Edit control not found after label 'P&assword:'.", "Injector", MB_OK | MB_ICONERROR);
        return;
    }

    std::wstring wpass(password.begin(), password.end());
    SendMessageW(passwordEdit, WM_SETTEXT, 0, (LPARAM)wpass.c_str());

    Sleep(1000); // Let the GUI update

    // Simulate ENTER key
    INPUT enterDown = { INPUT_KEYBOARD };
    enterDown.ki.wVk = VK_RETURN;
    INPUT enterUp = enterDown;
    enterUp.ki.dwFlags = KEYEVENTF_KEYUP;
    INPUT inputs[2] = { enterDown, enterUp };
    SendInput(2, inputs, sizeof(INPUT));
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

    Sleep(8000); // Wait for UI to render
    InjectPasswordDirect(password);

    MessageBoxA(NULL, "✅ Password injected successfully.", "Injector", MB_OK | MB_ICONINFORMATION);
    return 0;
}
