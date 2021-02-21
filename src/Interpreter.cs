using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace _4Pic.src
{
    public delegate void F(ref Interpreter obj);

    public class Interpreter
    {
        public const double TRUE = -1, FALSE = 0;
        public StackX<Double>[] stack;
        public int stack_cur;
        public bool error = false;
        public Image image;
        
        public bool Assert(bool flag, string s) {
            if (!flag) {
                MessageBox.Show(s, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error = true;
            }
            return !error;
        }

        public Interpreter(char[] program) {
            stack = new StackX<double>[2];
            stack_cur = 0;
        }

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
        internal static void floor(ref Interpreter state) {
            state.Push(Math.Floor(state.Pop()));
        }

        internal static void ceil(ref Interpreter state) {
            state.Push(Math.Ceiling(state.Pop()));
        }

        internal static void within(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            var x = state.Pop();
            state.Push((a <= x && x <= b)? TRUE: FALSE);
        }

        internal static void pow(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(Math.Pow(a, b));
        }

        internal static void inc(ref Interpreter state) {
            state.Push(state.Pop() + 1);
        }

        internal static void dec(ref Interpreter state) {
            state.Push(state.Pop() - 1);
        }

        internal static void plus(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a + b);
        }

        internal static void minus(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a - b);
        }

        internal static void mul(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a * b);
        }

        internal static void divide(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            if (state.Assert(b != 0, "/%: На ноль делить нельзя!")) {
                state.Push(a % b, a / b);
            }
        }

        internal static void and(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push((uint)a & (uint)b);
        }

        internal static void or(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push((uint)a | (uint)b);
        }

        internal static void xor(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push((uint)a ^ (uint)b);
        }

        internal static void abs(ref Interpreter state) {
            state.Push(Math.Abs(state.Pop()));
        }

        internal static void neg(ref Interpreter state) {
            state.Push(-state.Pop());
        }

        internal static void not(ref Interpreter state) {
            state.Push(state.Pop() == TRUE? FALSE: TRUE);
        }

        //
        // Stack functions
        //
        internal static void dropall(ref Interpreter state) {
            state.stack[state.stack_cur].Clear();
        }

        internal static void sdropall(ref Interpreter state) {
            state.stack[state.stack_cur ^ 1].Clear();
        }

        internal static void drop(ref Interpreter state) {
            state.Pop();
        }

        internal static void sdrop(ref Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            sstack.Pop();
        }

        internal static void dup(ref Interpreter state) {
            var x = state.Pop();
            state.Push(x, x);
        }

        internal static void qdup(ref Interpreter state) {
            var x = state.Pop();
            state.Push(x);
            if (x != FALSE)
                state.Push(x);
        }

        internal static void swap(ref Interpreter state) {
            var b = state.Pop();
            var a = state.Pop();
            state.Push(a, b);
        }

        internal static void rot(ref Interpreter state) {
            var c = state.Pop();
            var b = state.Pop();
            var a = state.Pop();
            state.Push(b, c, a);
        }

        internal static void pick(ref Interpreter state) {
            var stack = state.stack[state.stack_cur];
            var i = (int)stack.Pop();
            if ( state.Assert(i >= 0 && i < stack.Count,
                 "PICK: индекс выходит за границы стека!") ) {
                stack.Add(stack.ElementAt(stack.Count - i - 1));
            }
        }

        internal static void roll(ref Interpreter state) {
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

        internal static void froms(ref Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            state.Push(sstack.Pop());
        }

        internal static void tos(ref Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            sstack.Push(state.Pop());
        }

        internal static void copys(ref Interpreter state) {
            var sstack = state.stack[state.stack_cur ^ 1];
            state.Push(sstack.Peek());
        }

        internal static void sswitch(ref Interpreter state) {
            state.stack_cur ^= 1;
        }

        //
        // Control functions
        //
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

        //
        // Values functions
        //
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

        //
        // IO functions
        //
        internal static void input(ref Interpreter state) {
            throw new NotImplementedException();
        }
    }
}
