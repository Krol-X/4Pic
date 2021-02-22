using System;
using System.Collections;
using System.Collections.Generic;

namespace _4Pic.src
{
    using VMTable = Dictionary<byte, ArrayList>;

    public static class _4lang
    {
        public static VMTable vmtable = new VMTable {
			// Mathemathic functions
			{0x10, new ArrayList { "floor",  (F)Interpreter.floor } },
			{0x11, new ArrayList { "ceil",   (F)Interpreter.ceil} },
			{0x12, new ArrayList { "within", (F)Interpreter.within} },
			{0x13, new ArrayList { "power",  (F)Interpreter.pow} },
			{0x14, new ArrayList { "1+",     (F)Interpreter.inc} },
			{0x15, new ArrayList { "1-",     (F)Interpreter.dec} },
			{0x16, new ArrayList { "+",      (F)Interpreter.plus} },
			{0x17, new ArrayList { "-",      (F)Interpreter.minus} },
			{0x18, new ArrayList { "*",      (F)Interpreter.mul} },
			{0x19, new ArrayList { "/%",     (F)Interpreter.divide} },
			{0x1A, new ArrayList { "and",    (F)Interpreter.and} },
			{0x1B, new ArrayList { "or",     (F)Interpreter.or} },
			{0x1C, new ArrayList { "xor",    (F)Interpreter.xor} },
			{0x1D, new ArrayList { "abs",    (F)Interpreter.abs} },
			{0x1E, new ArrayList { "neg",    (F)Interpreter.neg} },
			{0x1F, new ArrayList { "not",    (F)Interpreter.not} },
			// Stack functions
			{0x20, new ArrayList { "dropall",  (F)Interpreter.dropall} },
			{0x21, new ArrayList { "sdropall", (F)Interpreter.sdropall} },
			{0x22, new ArrayList { "drop",     (F)Interpreter.drop} },
			{0x23, new ArrayList { "sdrop",    (F)Interpreter.sdrop} },
			{0x24, new ArrayList { "dup",      (F)Interpreter.dup} },
			{0x25, new ArrayList { "?dup",     (F)Interpreter.qdup} },
			{0x26, new ArrayList { "swap",     (F)Interpreter.swap} },
			{0x27, new ArrayList { "rot",      (F)Interpreter.rot} },
			{0x28, new ArrayList { "pick",     (F)Interpreter.pick} },
			{0x29, new ArrayList { "roll",     (F)Interpreter.roll} },
			{0x2A, new ArrayList { ">s",       (F)Interpreter.tos} },
			{0x2B, new ArrayList { "s>",       (F)Interpreter.froms} },
			{0x2C, new ArrayList { "s@",       (F)Interpreter.copys} },
			{0x2D, new ArrayList { "s-switch", (F)Interpreter.sswitch} },
			// Control functions
			{0x40, new ArrayList { "if",    (F)Interpreter._qbranch} }, 
			{0x41, new ArrayList { "else",  (F)Interpreter._branch} },
			{0x42, new ArrayList { "then",  (F)Interpreter.nop} },
			{0x43, new ArrayList { "exit",  (F)Interpreter.exit} },
			{0x44, new ArrayList { "?exit", (F)Interpreter.qexit} },
			{0x45, new ArrayList { "",      (F)Interpreter.call} },
            {0x48, new ArrayList { ":",     (F)Interpreter.define} },
            {0x49, new ArrayList { ";",     (F)Interpreter.enddefine} },
            {0x4F, new ArrayList { "stop",  (F)Interpreter.stop} },
			// Values functions
			{0x50, new ArrayList { "get:",  (F)Interpreter.getvar} },
			{0x51, new ArrayList { "set:",  (F)Interpreter.setvar} },
			{0x52, new ArrayList { "",      (F)Interpreter.lit} },
			{0x53, new ArrayList { "",      (F)Interpreter.litsz} },
			// IO functions
			{0x60, new ArrayList { "input", (F)Interpreter.input} }
        };
    }
}
