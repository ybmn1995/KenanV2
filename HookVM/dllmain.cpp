#include <windows.h>
#include "MinHook.h"

typedef int (WINAPI* MessageBoxA_t)(HWND, LPCSTR, LPCSTR, UINT);
MessageBoxA_t originalMessageBoxA = NULL;

int WINAPI HookedMessageBoxA(HWND hWnd, LPCSTR lpText, LPCSTR lpCaption, UINT uType) {
    return originalMessageBoxA(hWnd, "Hooked by MinHook!", "Intercepted", uType);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved) {
    if (ul_reason_for_call == DLL_PROCESS_ATTACH) {
        MH_Initialize();
        MH_CreateHook(&MessageBoxA, &HookedMessageBoxA, reinterpret_cast<LPVOID*>(&originalMessageBoxA));
        MH_EnableHook(&MessageBoxA);
    } else if (ul_reason_for_call == DLL_PROCESS_DETACH) {
        MH_DisableHook(&MessageBoxA);
        MH_Uninitialize();
    }
    return TRUE;
}
