using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliMood.Services
{
    public interface IEmotionGetter
    {
        string GetEmotionFromText(string text);
    }
}
