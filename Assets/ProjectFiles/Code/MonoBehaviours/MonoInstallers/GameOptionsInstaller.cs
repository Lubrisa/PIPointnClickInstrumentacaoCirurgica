using System.Collections.Generic;
using PointnClick;
using UnityEngine;
using Zenject;

public class GameOptionsInstaller : MonoInstaller
{
    [SerializeField] private List<ToolData> m_toolsData;

    public override void InstallBindings()
    {
        Container.Bind<List<ToolData>>().FromInstance(m_toolsData).AsSingle();
        Container.Bind<OperationType>().FromInstance(GameOptionsHolder.Instance.OperationType).AsSingle();
    }
}