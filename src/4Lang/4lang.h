#ifndef _4lang_h_
#define _4lang_h_

typedef struct {
    uint8_t name[16];
    uint16_t size;
    uint16_t reserved;
    uint8_t* code;
} TDefinition, *PDefenition;

#endif

