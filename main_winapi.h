TCHAR szError[MAX_LOADSTRING],
      szWClsMain[MAX_LOADSTRING],
      szMainTitle[MAX_LOADSTRING],
      szAbout[MAX_LOADSTRING],
      szAboutTitle[MAX_LOADSTRING];
HACCEL hAccelTable;

//
//   FUNCTION: int _tWinMain(HINSTANCE, HINSTANCE, LPTSTR, int)
//
//   REMARK: entry point for the application
//
int APIENTRY _tWinMain(HINSTANCE hInst, HINSTANCE hPrevInst,
                       LPTSTR    lpCmdLine, int   nCmdShow) {
    UNREFERENCED_PARAMETER(hPrevInst);  // Skip parameter for compatibility
    UNREFERENCED_PARAMETER(lpCmdLine);  // Ignore commandline
    HWND hWnd;

    // Loading vars and resources
    LoadString(hInst, IDS_ERROR,      szError,      MAX_LOADSTRING);
    LoadString(hInst, IDS_WCLS_MAIN,  szWClsMain,   MAX_LOADSTRING);
    LoadString(hInst, IDS_MAIN_TITLE, szMainTitle,  MAX_LOADSTRING);
    LoadString(hInst, IDS_ABOUT,      szAbout,      MAX_LOADSTRING);
    LoadString(hInst, IDS_ABOUTTITLE, szAboutTitle, MAX_LOADSTRING);

    hAccelTable = LoadAccelerators(hInst, TEXT("APP_ACCELERATORS"));

    // Register window class
    assert(win_regclass(hInst, &WCLS_main, szWClsMain),
           "Cannot register window class!");

    // Create window and control units
    assert(hWnd = WinMain_init(hInst, szWClsMain, szMainTitle),
           "Cannot create main window!");

    MSG msg;
    // Message handle loop
    while (GetMessage(&msg, NULL, 0, 0)) {
        if (!TranslateAccelerator(hWnd, hAccelTable, &msg)) {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }
    return (int) msg.wParam;
}

