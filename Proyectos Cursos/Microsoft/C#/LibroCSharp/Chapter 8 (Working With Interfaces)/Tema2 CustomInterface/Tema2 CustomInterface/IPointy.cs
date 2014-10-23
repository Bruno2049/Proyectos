using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_CustomInterface
{
    public interface IPointy
    {
        byte Points { get; }
        byte GetNumberOfPoints();
    }

    public interface IDrawToForm
    {
        void Draw();
    }
    // Draw to buffer in memory.
    public interface IDrawToMemory
    {
        void Draw();
    }
    // Render to the printer.
    public interface IDrawToPrinter
    {
        void Draw();
    }
}
