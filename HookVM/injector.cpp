#include <windows.h>
#include <tlhelp32.h>
#include <iostream>
#include <fstream>
#include <string>

std::wstring GetVMPathFromEnv() {
    std::ifstream envFile(".env");
    std::string line;
    while (std::getline(envFile, line)) {
        if (line.find("VMPath=") == 0) {
            std::wstring wpath(line.begin() + 7, line.end());
            return wpath;
        }
    }
    return L"";
}

DWORD StartVMPlayerAndGetPID(const std::wstring& vmxPath) {
    STARTUPINFOW si = { sizeof(si) };
    PROCESS_INFORMATION pi;
    std::wstring command = L""C:\Program Files (x86)\VMware\VMware Player\vmplayer.exe" "" + vmxPath + L""";
    if (CreateProcessW(NULL, &command[0], NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi)) {
        CloseHandle(pi.hThread);
        return pi.dwProcessId;
    }
    return 0;
}

bool InjectDLL(DWORD processId, const std::string& dllPath) {
    HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, processId);
    if (!hProcess) return false;

    LPVOID pDllPath = VirtualAllocEx(hProcess, NULL, dllPath.size() + 1, MEM_COMMIT, PAGE_READWRITE);
    if (!pDllPath) {
        CloseHandle(hProcess);
        return false;
    }

    WriteProcessMemory(hProcess, pDllPath, dllPath.c_str(), dllPath.size() + 1, NULL);

    HANDLE hThread = CreateRemoteThread(hProcess, NULL, 0,
        (LPTHREAD_START_ROUTINE)GetProcAddress(GetModuleHandleA("kernel32.dll"), "LoadLibraryA"),
        pDllPath, 0, NULL);

    if (!hThread) {
        VirtualFreeEx(hProcess, pDllPath, 0, MEM_RELEASE);
        CloseHandle(hProcess);
        return false;
    }

    WaitForSingleObject(hThread, INFINITE);
    VirtualFreeEx(hProcess, pDllPath, 0, MEM_RELEASE);
    CloseHandle(hThread);
    CloseHandle(hProcess);
    return true;
}

int main() {
    std::wstring vmxPath = GetVMPathFromEnv();
    std::string dllPath = "HookDLL.dll";

    DWORD pid = StartVMPlayerAndGetPID(vmxPath);
    if (pid == 0) {
        std::cout << "Failed to start VMware Player." << std::endl;
        return 1;
    }

    Sleep(4000); // Let VM initialize

    if (InjectDLL(pid, dllPath)) {
        std::cout << "DLL injected successfully." << std::endl;
    } else {
        std::cout << "DLL injection failed." << std::endl;
    }

    return 0;
}
