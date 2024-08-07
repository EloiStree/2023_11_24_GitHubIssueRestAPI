using UnityEngine;
using UnityEngine.Events;

public class GitHubUIMono_Reaction : MonoBehaviour
{
    public STRUCT_GitHubReaction m_reaction;


    [ContextMenu("Refresh UI")]
    public void RefreshUI()
    {
        m_onLike.Invoke(m_reaction.m_like);
        m_onDislike.Invoke(m_reaction.m_dislike);
        m_onTotal.Invoke(m_reaction.m_total);
        m_onHeart.Invoke(m_reaction.m_heart);
        m_onLaugh.Invoke(m_reaction.m_laugh);
        m_onConfused.Invoke(m_reaction.m_confused);
        m_onHooray.Invoke(m_reaction.m_hooray);
        m_onRocket.Invoke(m_reaction.m_rocket);
        m_onEyes.Invoke(m_reaction.m_eyes);
        string unicode=string.Format($"" +
            $"\t↑{m_reaction.m_like} " +
            $"\t↓{m_reaction.m_dislike} " +
            $"\tT {m_reaction.m_total} " +
            $"\t♥{m_reaction.m_heart} " +
            $"\t^^{m_reaction.m_laugh} " +
            $"\t(°O°){m_reaction.m_confused} " +
            $"\t\\(^^)/{m_reaction.m_hooray} " +
            $"\t-#=> {m_reaction.m_rocket} " +
            $"\t(oO){m_reaction.m_eyes}");
        m_unicodeReaction.Invoke(unicode);
    }

    [Header("Like")]
    public UnityEvent<int> m_onLike;
    public UnityEvent<int> m_onDislike;
    public UnityEvent<int> m_onTotal;
    [Header("Other")]
    public UnityEvent<int> m_onHeart;
    public UnityEvent<int> m_onLaugh;
    public UnityEvent<int> m_onConfused;
    public UnityEvent<int> m_onHooray;
    public UnityEvent<int> m_onRocket;
    public UnityEvent<int> m_onEyes;

    [Header("Unicode")]
    public UnityEvent<string> m_unicodeReaction;
}

[System.Serializable]
public struct STRUCT_GitHubReaction
{
    public int m_like;
    public int m_dislike;
    public int m_heart;
    public int m_laugh;
    public int m_confused;
    public int m_hooray;
    public int m_rocket;
    public int m_eyes;
    public int m_total;
}