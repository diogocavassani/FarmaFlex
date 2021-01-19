using System.ComponentModel;

namespace APIFarmaFlex.Domain.Models
{
    public enum FuncaoEnum : byte
    {
        [Description("Usuario")]
        Usuario = 1,
        [Description("Cliente")]
        Operador = 2,
        
    }
}
