//
//   FUNCTION: int _tWinMain(HINSTANCE, HINSTANCE, LPTSTR, int)
//
//   REMARK: entry point for the application
//h
int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
                       LPTSTR    lpCmdLine, int       nCmdShow) {
	UNREFERENCED_PARAMETER(hPrevInstance);  // Skip parameter for compatibility
	UNREFERENCED_PARAMETER(lpCmdLine);      // Ignore commandline
	HWND hWnd;
	/*
		// Loading vars and resources
		LoadString(hInstance, IDS_WNDCLASS, szWindowClass, MAX_LOADSTRING);
		LoadString(hInstance, IDS_ABOUTTITLE, szAboutTitle, MAX_LOADSTRING);
		LoadString(hInstance, IDS_ABOUT, szAbout, MAX_LOADSTRING);
		LoadString(hInstance, IDS_ERROR, szError, MAX_LOADSTRING);
		LoadString(hInstance, IDDS_OPENTITLE, szOpenDlgTitle, MAX_LOADSTRING);
		hAccelTable = LoadAccelerators(hInstance, TEXT("APP_ACCELERATORS"));

		// Register window class
		if (!RegisterWndClass())
			return false;

		// Create window and control units
		hWnd = InitWnd();
		if (!hWnd)
			return false;

		MSG msg;
		// Message handle loop
		while (GetMessage(&msg, NULL, 0, 0)) {
			if (!TranslateAccelerator(hWnd, hAccelTable, &msg)) {
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
		}
		return (int) msg.wParam;
	*/
	return 0;
}

