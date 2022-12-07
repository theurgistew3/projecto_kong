using UnityEngine;
using UnityEngine.UI;

namespace fisher.game_manager
{
	public class Game_manager_fisher : MonoBehaviour
	{
		public static Game_manager_fisher sharedInstance;

		public int max_number_of_band_fish = 3;

		public Text scoreText;
		int scorePlayer = 0;
		int maxScorePlayer;

		public chibi.joystick.Joystick_fisher joystick;
		public Text end_score, high_score;

		public Canvas canvasPause, canvasGameOver, push_start, game_hub;

		// Start is called before the first frame update
		void Start()
		{
			maxScorePlayer = PlayerPrefs.GetInt( "MaxScore", 0 );
			scoreText.text = "Score: " + scorePlayer;
			joystick.enabled = false;
			int width = 432; // or something else
			int height = 768; // or something else
			bool isFullScreen = false; // should be windowed to run in arbitrary resolution
			int desiredFPS = 60; // or something else

			Screen.SetResolution( width, height, isFullScreen, desiredFPS );
		}



		// Update is called once per frame
		void Update()
		{
			scoreText.text = "Score: " + scorePlayer;
		}

		// Para boton de Pausa
		public void PauseGame()
		{
			// Detener tiempo
			Time.timeScale = 0;
			// Mostrar canvas 'Pausa'
			canvasPause.enabled = true;
		}

		// Regresar de Pausa al 'Juego'
		public void BackGame()
		{
			// Continuar tiempo
			Time.timeScale = 1;
			// Cerrar canvas 'Pausa'
			canvasPause.enabled = false;
		}

		public void AddPointsScore()
		{
			// Sumar al score
			scorePlayer++;
			// Muestra en pantalla
			scoreText.text = "Score: " + scorePlayer;
		}

		// Mostar Game Over
		public void ShowGameOver()
		{
			if ( scorePlayer > maxScorePlayer )
			{
				PlayerPrefs.SetInt( "MaxScore", scorePlayer );
				maxScorePlayer = scorePlayer;
			}

			high_score.text = "High Score: " + maxScorePlayer;

			end_score.text = scorePlayer.ToString();

			Time.timeScale = 0;
			canvasGameOver.enabled = true;
			game_hub.enabled = false;
		}

		public void add_band_fish()
		{
			if ( --max_number_of_band_fish <= 0 )
				ShowGameOver();
		}

		public void start_game()
		{
			scorePlayer = 0;
			push_start.enabled = false;
			game_hub.enabled = true;
			canvasGameOver.enabled = false;
			scoreText.text = "Score: " + scorePlayer;
			joystick.enabled = true;
			Time.timeScale = 1;

			max_number_of_band_fish = 3;
		}
	}
}
