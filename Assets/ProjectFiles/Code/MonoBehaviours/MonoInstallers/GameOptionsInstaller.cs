using System.Collections.Generic;
using PointnClick;
using UnityEngine;
using Zenject;

public class GameOptionsInstaller : MonoInstaller
{
    [SerializeField] private List<ToolData> m_toolsData;

    public override void InstallBindings()
    {
        GameData gameOptions = GameOptionsHolder.Instance.GetData();

        Container.Bind<List<ToolData>>().FromInstance(m_toolsData).AsSingle();
        Container.Bind<OperationType>().FromInstance(gameOptions.Operation).AsSingle();
        Container.Bind<int>().FromInstance(gameOptions.ToolsQuantity).AsSingle();
    }
}