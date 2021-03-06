using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class MyGameManager : Photon.PunBehaviour {

	static public MyGameManager Instance;

	[Tooltip("Préfab qui représente le joueur")]
	public GameObject playerPrefab;

	public GameObject playersText;
	public GameObject kunaiUI;
	public GameObject shurikenUI;
	public GameObject bombUI;
	public GameObject ultimateUI;

	void Start() {
		// Instancier le gamemanager
		Instance = this;

		if (playerPrefab == null) 
		{
			Debug.LogError("<Color=Red><a>Il manque</a></Color> une référence pour la préfab du joueur. Remettre le game object",this);
		} 
		else
		{
			if (TaichiPlayerManager.LocalPlayerInstance==null)
			{
				Debug.Log("Instanciation du joueur local");
				PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0.5f,0f), Quaternion.identity, 0);
			}
			else
			{
				Debug.Log("Ignorer le chargement principal");
			}
		}

		updatePlayerList ();
	}
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene(0);
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer other)
	{
		Debug.Log("Connexion établie " + other.NickName); // On ne le voit pas si on est le joueur qui se connecte

		updatePlayerList ();
	}
	public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
	{
		Debug.Log("Déconnexion " + other.NickName); //Voir quand l'autre joueur se déconnecte
		updatePlayerList ();
	}
	public void LeaveRoom()
	{
		Debug.Log ("Quitter la partie");
		PhotonNetwork.LeaveRoom();
	}

	void updatePlayerList() {
		string players = "";
		foreach(PhotonPlayer pl in PhotonNetwork.playerList)
		{
			players += pl.NickName + "\n";
		}
		playersText.GetComponent<Text> ().text = players;
	}

	public void updateUI(int k, int s, int b, int u) {//Power
		kunaiUI.GetComponent<Text> ().text = "x " + k;
		shurikenUI.GetComponent<Text> ().text = "x " + s;
		bombUI.GetComponent<Text> ().text = "x " + b;
		ultimateUI.GetComponent<Text> ().text = "x " + u;

	}
}
