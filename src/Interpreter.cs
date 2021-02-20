using System;
using System.Collections.Generic;
using System.Drawing;

namespace _4Pic.src
{
    public delegate void F(ref Interpreter obj);

    public class Interpreter
    {
        public Stack<Double>[] stack;
        public int stack_cur;
        public Image image;

        public Interpreter(char[] program) {
            stack = new Stack<double>[2];
            stack_cur = 0;
        }

        // Stack internal functions

        void push(Double x) {
            stack[stack_cur].Push(x);
        }

        Double pop() {
            return stack[stack_cur].Pop();
        }


        // Mathemathic functions

        internal static void floor(ref Interpreter state) {
            state.push(Math.Floor(state.pop()));
        }

        internal static void ceil(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void within(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void pow(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void inc(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void dec(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void plus(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void minus(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void mul(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void divide(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void and(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void or(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void xor(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void abs(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void neg(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void not(ref Interpreter state) {
            throw new NotImplementedException();
        }

        // Stack functions

        internal static void dropall(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void sdropall(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void drop(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void sdrop(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void dup(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void qdup(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void swap(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void rot(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void pick(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void roll(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void froms(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void tos(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void copys(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void sswitch(ref Interpreter state) {
            throw new NotImplementedException();
        }

        // Control functions

        internal static void _if(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void _else(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void _then(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void exit(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void qexit(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void call(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void define(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void enddefine(ref Interpreter state) {
            throw new NotImplementedException();
        }

        // Values functions

        internal static void getvar(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void setvar(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void lit(ref Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void litsz(ref Interpreter state) {
            throw new NotImplementedException();
        }

        // IO functions

        internal static void input(ref Interpreter state) {
            throw new NotImplementedException();
        }
    }
}
