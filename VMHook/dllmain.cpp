#include <windows.h>
#include <detours.h>

static int (WINAPI* TrueMessageBoxA)(HWND, LPCSTR, LPCSTR, UINT) = MessageBoxA;

int WINAPI MyMessageBoxA(HWND hWnd, LPCSTR lpText, LPCSTR lpCaption, UINT uType) {
    return TrueMessageBoxA(hWnd, "Hooked via Detours!", "Intercepted", uType);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID reserved) {
    if (reason == DLL_PROCESS_ATTACH) {
        DisableThreadLibraryCalls(hModule);
        DetourTransactionBegin();
        DetourUpdateThread(GetCurrentThread());
        DetourAttach(&(PVOID&)TrueMessageBoxA, MyMessageBoxA);
        DetourTransactionCommit();
    }
    else if (reason == DLL_PROCESS_DETACH) {
        DetourTransactionBegin();
        DetourUpdateThread(GetCurrentThread());
        DetourDetach(&(PVOID&)TrueMessageBoxA, MyMessageBoxA);
        DetourTransactionCommit();
    }
    return TRUE;
}
