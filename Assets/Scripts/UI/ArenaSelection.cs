using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArenaSelection : MonoBehaviour
{
    public List<Button> arenaButtons;

    private void Start()
    {
        // get playercontainer
        var playerContainerGo = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerGo && playerContainerGo.TryGetComponent(out PlayerContainer playerContainer))
        {
            foreach (var arena in playerContainer.arenas)
            {
                if (!playerContainer.IsArenaUnlocked(arena)) continue;

                var arenaButton = arenaButtons.Find(x => x.name == arena.sceneName);
                if (arenaButton)
                {
                    arenaButton.interactable = true;
                }
            }
        }
    }
}