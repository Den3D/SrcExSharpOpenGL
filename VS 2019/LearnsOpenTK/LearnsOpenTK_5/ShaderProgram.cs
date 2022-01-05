using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace LearnsOpenTK_5
{
    class ShaderProgram
    {
        private readonly int _vertexShader = 0;
        private readonly int _fragmentShader = 0;
        private readonly int _program = 0;

        public ShaderProgram(string vertexfile, string fragmentfile)
        {
            _vertexShader = CreateShader(ShaderType.VertexShader, vertexfile);
            _fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentfile);

            _program = GL.CreateProgram();
            GL.AttachShader(_program, _vertexShader);
            GL.AttachShader(_program, _fragmentShader);

            GL.LinkProgram(_program);
            GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(_program);
                throw new Exception($"Ошибка линковки шейдерной программы № {_program} \n\n {infoLog}");
            }

            DeleteShader(_vertexShader);
            DeleteShader(_fragmentShader);
        }

        public void ActiveProgram() => GL.UseProgram(_program);
 
        public void DeactiveProgram() => GL.UseProgram(0);

        public void DeleteProgram() => GL.DeleteProgram(_program);
 
        private int CreateShader(ShaderType shaderType, string shaderFile)
        {
            string shaderStr = File.ReadAllText(shaderFile);
            int shaderID = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderID, shaderStr);
            GL.CompileShader(shaderID);

            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shaderID);
                throw new Exception($"Ошибка прикомпиляции шейдера № {shaderID} \n\n {infoLog}");
            }

            return shaderID;
        }

        private void DeleteShader(int shader)
        {
            GL.DetachShader(_program, shader);
            GL.DeleteShader(shader);
        }
    }
}
