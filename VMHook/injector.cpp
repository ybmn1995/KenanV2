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

// Recursively log all child windows with hierarchy
void LogAllChildWindows(HWND parent, std::wstring& log, int level = 0) {
    HWND child = NULL;
    wchar_t className[256];
    wchar_t windowText[256];

    while ((child = FindWindowExW(parent, child, NULL, NULL)) != NULL) {
        GetClassNameW(child, className, 256);
        GetWindowTextW(child, windowText, 256);

        std::wstring indent(level * 2, L' ');
        log += indent + L"↳ Class: " + std::wstring(className) + L" | Label: " + std::wstring(windowText) + L"\n";
        
        // Recursively inspect grandchildren
        LogAllChildWindows(child, log, level + 1);
    }
}

void InjectPasswordDirect(const std::string& password) {
    HWND vmwareMain = FindWindow(NULL, L"VMware Workstation 17 Player (Non-commercial use only)");
    if (!vmwareMain) {
        MessageBoxA(NULL, "❌ VMware window not found.", "Injector", MB_OK | MB_ICONERROR);
        return;
    }

    // Print all children window class names and labels
    std::wstring classLog = L"🔍 All Window Hierarchy:\n";
    LogAllChildWindows(vmwareMain, classLog);
    MessageBoxW(NULL, classLog.c_str(), L"Class + Label Hierarchy", MB_OK);

    // Injection not performed in this version – use your own hook logic if needed
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

    Sleep(8000); // Allow GUI to appear
    InjectPasswordDirect(password);

    return 0;
}
