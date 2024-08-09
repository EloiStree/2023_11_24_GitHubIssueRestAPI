using UnityEngine;

public struct STRUCT_GitHubLabelWithSideInfo : I_OwnGitHubLabelNamedAndIdGet, I_OwnGitHubLabelDescriptionGet, I_OwnGitHubLabelColorGet{

    public STRUCT_GitHubLabelId m_labelId;
    public string m_descriptionOfLabel;
    public Color m_labelColor;

    public Color GetLabelColor()
    {
        return m_labelColor;
    }

    public string GetLabelDescription()
    {
        return m_descriptionOfLabel;
    }

    public int GetLabelId()
    {
        return m_labelId.GetLabelId();
    }

    public string GetLabelName()
    {
        return m_labelId.GetLabelName();
    }
}
