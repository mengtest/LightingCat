using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static string WWW_STREAMING_ASSETS_PATH;
    public static string STREAMING_ASSETS_PATH;
    public static string PERSISTENT_DATA_PATH;
    //初始化数据
    public static void InitConfig()
    {
#if UNITY_EDITOR
    WWW_STREAMING_ASSETS_PATH = "file:///" + Application.streamingAssetsPath;
    STREAMING_ASSETS_PATH = Application.streamingAssetsPath;
    PERSISTENT_DATA_PATH = Application.dataPath + "/PersistentAssets";
#elif UNITY_STANDALONE_WIN
    WWW_STREAMING_ASSETS_PATH = "file:///"+Application.streamingAssetsPath;
    STREAMING_ASSETS_PATH = Application.streamingAssetsPath;
    PERSISTENT_DATA_PATH = Application.dataPath + "/PersistentAssets";
#elif UNITY_IPHONE
    WWW_STREAMING_ASSETS_PATH = "file:///"+Application.streamingAssetsPath;
    STREAMING_ASSETS_PATH = Application.streamingAssetsPath;
    PERSISTENT_DATA_PATH = Application.persistentDataPath;
#elif UNITY_ANDROID
    WWW_STREAMING_ASSETS_PATH = Application.streamingAssetsPath;
    STREAMING_ASSETS_PATH = Application.streamingAssetsPath;
    PERSISTENT_DATA_PATH = Application.persistentDataPath;
#endif
    }

//服务器IP
#if UNITY_EDITOR
public const string SERVER_IP = "127.0.0.1";
#elif UNITY_STANDALONE_WIN
    public const string SERVER_IP = "192.168.93.234";
#elif UNITY_IPHONE
    public const string SERVER_IP = "192.168.93.234";
#elif UNITY_ANDROID
    public const string SERVER_IP = "192.168.93.234";
#endif

    public const int SERVER_PORT = 8888;
    public const int KCP_SERVER_PORT = 9224;

    public const int MAX_BUFFER_SIZE = 32 * 1024;

    public const string AppPrefix = "";

    public const int MAX_REPLAY_COUNT = 10;

    //Logic 
    public readonly static int GAME_RATE = 60;
    //普通帧 几次的时间 为一逻辑帧
    public readonly static int FramesPerLocksetpTurn = 4;
    //普通帧间隔
    public readonly static float FrameSimpleLength = 16.5f;
    //逻辑帧间隔
    public readonly static int FrameLogicLength = (int)(FramesPerLocksetpTurn * FrameSimpleLength);
    //插值分段
    public readonly static float LerpInterval = 1f/FramesPerLocksetpTurn;
    //计算基数
    public static readonly int BASE_NUM = 1000;
}