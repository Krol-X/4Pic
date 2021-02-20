#ifndef opcodes_h_
#define opcodes_h_

enum {
    // Mathemathic functions
    op_floor  = 0x10,
    op_ceil   = 0x11,
    op_within = 0x12,
    op_pow    = 0x13,
    op_inc    = 0x14,
    op_dec    = 0x15,
    op_plus   = 0x16,
    op_minus  = 0x17,
    op_mul    = 0x18,
    op_divide = 0x19,
    op_and    = 0x1A,
    op_or     = 0x1B,
    op_xor    = 0x1C,
    op_abs    = 0x1D,
    op_neg    = 0x1E,
    op_not    = 0x1F,
    // Stack functions
    op_dropall  = 0x20,
    op_sdropall = 0x21,
    op_drop     = 0x22,
    op_sdrop    = 0x23,
    op_dup      = 0x24,
    op_qdup     = 0x25,
    op_swap     = 0x26,
    op_rot      = 0x27,
    op_pick     = 0x28,
    op_roll     = 0x29,
    op_tos      = 0x2A,
    op_froms    = 0x2B,
    op_copys    = 0x2C,
    // Control functions
    op_branch   = 0x40,
    op_qbranch  = 0x41,
    op_exit     = 0x42,
    op_qexit    = 0x43,
    op_call     = 0x44,
    // Values functions
    op_getvar   = 0x50,
    op_setvar   = 0x51,
    op_lit      = 0x52,
    op_litsz    = 0x53,
    // IO functions
    op_input    = 0x60
};

#endif

