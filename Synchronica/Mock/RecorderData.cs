/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2015 Wu Yuntao
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

namespace Synchronica.Mock
{
    public class RecorderData
    {
        public int StartTime;

        public int EndTime;

        public GameObjectData[] GameObjects;
    }

    public class GameObjectData
    {
        public int Id;

        public int StartTime;

        public VariableDefinition[] Definitions;

        public VariableData[] Variables;

        public int EndTime;
    }

    public enum VariableType
    {
        VBoolean,
        VInt16,
        VInt32,
        VInt64,
        VFloat,
    }

    public class VariableDefinition
    {
        public int Id;

        public VariableType Type;

        public object InitialValue;
    }

    public class VariableData
    {
        public int Id;

        public KeyFrameData[] KeyFrames;
    }

    public enum KeyFrameType
    {
        Linear_Int16,
        Linear_Int32,
        Linear_Int64,
        Linear_Float,

        Pulse_Int16,
        Pulse_Int32,
        Pulse_Int64,
        Pulse_Float,

        Step_Boolean,
        Step_Int16,
        Step_Int32,
        Step_Int64,
        Step_Float,
    }

    public class KeyFrameData
    {
        public int Time;

        public KeyFrameType Type;

        public object Value;
    }
}
