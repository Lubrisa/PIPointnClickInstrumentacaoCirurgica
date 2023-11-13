using System.Collections.Generic;
using PointnClick;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOptionsInstaller : MonoInstaller
{
    [SerializeField] private List<ToolData> m_toolsData;
    [SerializeField] private OperationType m_operation;

    public override void InstallBindings()
    {
        Container.Bind<List<ToolData>>().FromInstance(m_toolsData).AsSingle();

        if (GameOptionsHolder.Instance is not null)
            Container.Bind<OperationType>().FromInstance(GameOptionsHolder.Instance.OperationType).AsSingle();
        else
            Container.Bind<OperationType>().FromInstance(m_operation).AsSingle();
    }

    [ContextMenu("Load GameOver")]
    private void LoadGameOver() => SceneManager.LoadScene(4);

    [ContextMenu("Load Victory")]
    private void LoadVictoryScreen() => SceneManager.LoadScene(5);
}