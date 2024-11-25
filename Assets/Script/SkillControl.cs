using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControl : MonoBehaviour
{
    public ChargeThrow Granny;
    public ChargeThrow General;

    public void GeneralHealUsed(GameObject SkillObj) 
    {
        if (Gamemanager.instance.Skillable == true)
        {
            if (Gamemanager.instance.currentTurn == 0)
            {
                General.isHealing = true;
                Gamemanager.instance.Skillable = false;
                Destroy(SkillObj);
            }
        }
    }

    public void GrannyHealUsed(GameObject SkillObj)
    {
        if (Gamemanager.instance.Skillable == true)
        {
            if (Gamemanager.instance.currentTurn == 1)
            {
                Granny.isHealing = true;
                Gamemanager.instance.Skillable = false;
                Destroy(SkillObj);
            }
        }
    }

    public void GeneralDoubleUsed(GameObject SkillObj)
    {
        if (Gamemanager.instance.Skillable == true)
        {
            if (Gamemanager.instance.currentTurn == 0)
            {
                General.isDouble = true;
                Gamemanager.instance.Skillable = false;
                Destroy(SkillObj);
            }
        }
    }
    public void GrannyDoubleUsed(GameObject SkillObj)
    {
        if (Gamemanager.instance.Skillable == true)
        {
            if (Gamemanager.instance.currentTurn == 1)
            {
                Granny.isDouble = true;
                Gamemanager.instance.Skillable = false;
                Destroy(SkillObj);
            }
        }
    }

    public void GeneralHeavyUse(GameObject SkillObj)
    {
        if (Gamemanager.instance.Skillable == true)
        {
            if (Gamemanager.instance.currentTurn == 0)
            {
                General.isHeavyThrow = true;
                Gamemanager.instance.Skillable = false;
                Destroy(SkillObj);
            }
        }
    }
    public void GrannyHeavyUse(GameObject SkillObj)
    {
        if (Gamemanager.instance.Skillable == true)
        {
            if (Gamemanager.instance.currentTurn == 1)
            {
                Granny.isHeavyThrow = true;
                Gamemanager.instance.Skillable = false;
                Destroy(SkillObj);
            }
        }
    }
}

