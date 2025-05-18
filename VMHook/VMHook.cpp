//#include <windows.h>
//#include <detours.h>
//#include <fstream>
//#include <string>
//#include "PasswordRead.h"
//
//#pragma comment(lib, "detours.lib")
//
//
//BOOL(WINAPI* TrueSetWindowTextW)(HWND hWnd, LPCWSTR lpString) = SetWindowTextW;
//
//BOOL WINAPI HookedSetWindowTextW(HWND hWnd, LPCWSTR lpString) {
//    static bool injected = false;
//    wchar_t className[256] = { 0 };
//    GetClassNameW(hWnd, className, 256);
//
//    if (!injected && wcscmp(className, L"Edit") == 0) {
//        std::string password = ReadVMPassword();
//        std::wstring wpass(password.begin(), password.end());
//        injected = false; // Prevent multiple injection
//        return TrueSetWindowTextW(hWnd, wpass.c_str());
//    }
//
//    return TrueSetWindowTextW(hWnd, lpString);
//}
//
//BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID reserved) {
//    if (reason == DLL_PROCESS_ATTACH) {
//        DisableThreadLibraryCalls(hModule);
//        DetourTransactionBegin();
//        DetourUpdateThread(GetCurrentThread());
//        DetourAttach((PVOID*)&TrueSetWindowTextW, HookedSetWindowTextW);
//        DetourTransactionCommit();
//    }
//    else if (reason == DLL_PROCESS_DETACH) {
//        DetourTransactionBegin();
//        DetourUpdateThread(GetCurrentThread());
//        DetourDetach((PVOID*)&TrueSetWindowTextW, HookedSetWindowTextW);
//        DetourTransactionCommit();
//    }
//    return TRUE;
//}