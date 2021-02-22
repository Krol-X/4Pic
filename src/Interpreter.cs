using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace _4Pic.src
{
    public delegate void F(Interpreter obj);

    public class Interpreter
    {
        public const double TRUE = -1, FALSE = 0;

        public byte[] image;
        public StackX<Double>[] stack;
        public int stack_cur;
        public StackX<int> rstack;
        private byte[] mem;
        private int ip;
        private int if_depth;
        private bool f_running, f_error;

        public bool Assert(bool flag, string s) {
            if (!flag) {
                MessageBox.Show(s, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                f_error = true;
            }
            return !f_error;
        }

        public Interpreter(ref byte[] bytecode) {
            mem = bytecode;
            stack = new StackX<double>[2];
            stack_cur = 0;
            rstack = new StackX<int>();
            f_error = false;
        }

        public Image Evaluate(ref Image image) {
            this.image = Tools.imageToByteArray(ref image);
            this.if_depth = 0;
            this.f_error = false;
            this.ip = 0;
            this.f_running = false;
            while (f_running) {
                F fun = (F)_4lang.vmtable[mem[ip++]][1];
                fun(this);
            }
            return Tools.byteArrayToImage(ref this.image);
        }

        //
        // VM Primitives
        //

        internal static void nop(Interpreter state) { }

        // Stack internal functions

        void Push(params Double[] elements) {
            foreach (var x in elements) {
                stack[stack_cur].Push(x);
            }          
        }

        double Pop() {
            return stack[stack_cur].Pop();
        }

        double Peek() {
            return stack[stack_cur].Peek();
        } 

        //
        // Mathemathic functions
        //
        internal static void floor(Interpreter state) {
            state.Push(Math.Floor(state.Pop()));
        }

        internal static void ceil(Interpreter state) {
            state.Push(Math.Ceiling(state.Pop()));
        }

        internal static void within(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            var x = state.Pop();
            state.Push((a <= x && x <= b)? TRUE: FALSE);
        }

        internal static void pow(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(Math.Pow(a, b));
        }

        internal static void inc(Interpreter state) {
            state.Push(state.Pop() + 1);
        }

        internal static void dec(Interpreter state) {
            state.Push(state.Pop() - 1);
        }

        internal static void plus(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a + b);
        }

        internal static void minus(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a - b);
        }

        internal static void mul(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a * b);
        }

        internal static void divide(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            if (state.Assert(b != 0, "/%: На ноль делить нельзя!")) {
                state.Push(a % b, a / b);
            }
        }

        internal static void and(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push((int)a & (int)b);
        }

        internal static void or(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push((int)a | (int)b);
        }

        internal static void xor(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push((int)a ^ (int)b);
        }

        internal static void abs(Interpreter state) {
            state.Push(Math.Abs(state.Pop()));
        }

        internal static void neg(Interpreter state) {
            state.Push(-state.Pop());
        }

        internal static void not(Interpreter state) {
            state.Push(state.Pop() == TRUE? FALSE: TRUE);
        }

        //
        // Stack functions
        //
        internal static void dropall(Interpreter state) {
            state.stack[state.stack_cur].Clear();
        }

        internal static void sdropall(Interpreter state) {
            state.stack[state.stack_cur ^ 1].Clear();
        }

        internal static void drop(Interpreter state) {
            state.Pop();
        }

        internal static void sdrop(Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            sstack.Pop();
        }

        internal static void dup(Interpreter state) {
            var x = state.Pop();
            state.Push(x, x);
        }

        internal static void qdup(Interpreter state) {
            var x = state.Pop();
            state.Push(x);
            if (x != FALSE)
                state.Push(x);
        }

        internal static void swap(Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a, b);
        }

        internal static void rot(Interpreter state) {
            var c = state.Pop();
            var b = state.Pop();
            var a = state.Pop();
            state.Push(b, c, a);
        }

        internal static void pick(Interpreter state) {
            var stack = state.stack[state.stack_cur];
            var i = (int)stack.Pop();
            if ( state.Assert(i >= 0 && i < stack.Count,
                 "PICK: индекс выходит за границы стека!") ) {
                stack.Add(stack.ElementAt(stack.Count - i - 1));
            }
        }

        internal static void roll(Interpreter state) {
            var stack = state.stack[state.stack_cur];
            var i = (int)stack.Pop();
            if (state.Assert(i >= 0 && i < stack.Count,
                 "ROLL: индекс выходит за границы стека!")) {
                i = stack.Count - i - 1;
                var x = stack.ElementAt(i);
                stack.RemoveAt(i);
                stack.Add(i);
            }
        }

        internal static void froms(Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            state.Push(sstack.Pop());
        }

        internal static void tos(Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            sstack.Push(state.Pop());
        }

        internal static void copys(Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            state.Push(sstack.Peek());
        }

        internal static void sswitch(Interpreter state) {
            state.stack_cur ^= 1;
        }

        //
        // Control functions
        //
        internal static void _branch(Interpreter state) {
            
        }

        internal static void _qbranch(Interpreter state) {
            if (state.Assert(state.if_depth > 0, "ELSE: не был открыт IF!")) {
                state.if_depth--;
                state.f_scan_endif = !state.f_scan_endif;
            }
        }

        internal static void exit(Interpreter state) {
            var stack = state.rstack;
            if (stack.Count > 0) {
                state.ip = (int)stack.Pop();
            } else {
                state.f_running = false;
            }
        }

        internal static void qexit(Interpreter state) {
            if (state.Pop() == FALSE) {
                exit(state);
            }
        }

        internal static void call(Interpreter state) {
            state.rstack.Push(state.ip);
            state.ip = state.mem[state.ip++];
        }

        internal static void define(Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void enddefine(Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void stop(Interpreter state) {
            state.f_running = false;
        }

        //
        // Values functions
        //
        internal static void getvar(Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void setvar(Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void lit(Interpreter state) {
            throw new NotImplementedException();
        }

        internal static void litsz(Interpreter state) {
            throw new NotImplementedException();
        }

        //
        // IO functions
        //
        internal static void input(Interpreter state) {
            throw new NotImplementedException();
        }
    }
}
