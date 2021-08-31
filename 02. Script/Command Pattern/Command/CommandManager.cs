using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager
{
    private Dictionary<string, TowerCommand> commandDic = new Dictionary<string, TowerCommand>();


    //커맨드를 세팅
    public void SetCommand(string name, TowerCommand command)
    {
        if (commandDic.ContainsValue(command))
        {
            Debug.Log("이미 커맨드가 리스트 포함되어있음.");
            return;
        }
        commandDic.Add(name, command);
    }

    //저장된 특정 커맨드를 실행
    public void InvokeExecute(string name)
    {
        commandDic[name].Execute();
    }
}
