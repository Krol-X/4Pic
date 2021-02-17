#include "include.h"
#include "tools.h"

extern TCHAR szError[MAX_LOADSTRING];

void assert(bool f, TCHAR *s) {
	if (!f) {
		TCHAR str[MAX_LOADSTRING];
		sprintf(str, "Fatal error: %s", s);
		fprintf(stderr, str);
		MessageBox(NULL, str, szError, MB_ICONERROR | MB_OK);
		exit(EXIT_FAILURE);
	}
}

