
using System;

namespace Function
{
    public enum GameResultType
    {
        Great,
        Normal,
        Bad
    }

    public enum UnitType
    {
        Innocent,
        Monster
    }

    public enum UnitAnimaType
    {
        Idle,
        InWalk,
        OutWalk,
        Stop
    }

    public enum SoundType
    {
        Bgm,
        Sfx
    }

    public enum QuestionType
    {
        Name,
        Specific,
        Age,
        Job,
        Height,
        Residence,
        Weight,
        Info
    }

    public enum TriggerType
    {
        Click,
        Hover,
        Descend
    }

    public enum SceneType
    {
        MainSetting,
        Title,
        Loading,
        Game,
        Lobby,
        Result
    }

    [Flags]
    public enum UIKeyword
    {
        None = 0,
        Button = 1,
        Panel = 2,
        Slider = 4,
        Toggle = 8,
        DropDown = 16,
        CheckBox = 32,
        Label = 64,
        SceneChange = 128,
        Exit = 256,
        Selection = 512,
        Setup = 1024,
        Init = 2048,
        Deco = 4096
    }
}
