using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    public Image face;
    public Sprite[] faces; // Smile, Cool, Dead
    public Image[] timer; // 100s, 10s, 1s
    public Image[] bombCounter; // 100s, 10s, 1s
    public Sprite[] digits; // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, -
    private Image[,] grid;
    public Sprite[] places; // Untouched, Numbers, Empty, Flag, Bomb, BombExploded, FlagWrong
    private int[,] board;
    private bool complete = false;
    private bool timing = false;
    private int time = 0;
    private int bombCount = 10;
    public AudioSource aud;
    public AudioClip[] sounds; // Start, Lose, Win

    private void Awake() {
        grid = new Image[9, 9];
        for (int y = 0; y < 9; y++) {
            for (int x = 0; x < 9; x++) {
                grid[y, x] = GameObject.Find(y + "," + x).GetComponent<Image>();
            }
        }
        board = new int[9, 9];
    }

    private void OnEnable() {
        NewBoard();
    }

    private void OnDisable() {
        complete = true;
        timing = false;
        CancelInvoke("UpdateTimer");
    }

    private void NewBoard() {
        void GenerateBoardNumbers() {
            int CountBombs(int yPos, int xPos) {
                int count = 0;
                for (int y = (yPos-1); y < (yPos+2); y++) {
                    for (int x = (xPos-1); x < (xPos+2); x++) {
                        if (y >= 0 && y < 9 && x >= 0 && x < 9 && board[y, x] == -1) {
                            count++;
                        }
                    }
                }
                return count;
            }
            List<(int, int)> pairs = new List<(int, int)> ();
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    board[y, x] = 0;
                    pairs.Add((y, x));
                }
            }
            // Pick 10 random bomb positions
            for (int i = 0; i < 10; i++) {
                int selectedIndex = Random.Range(0, pairs.Count - 1);
                board[pairs[selectedIndex].Item1, pairs[selectedIndex].Item2] = -1;
                pairs.RemoveAt(selectedIndex);
            }
            // Calculate board numbers
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    if (board[y, x] != -1) {
                        // Count surrounding bombs
                        board[y, x] = CountBombs(y, x);
                    }
                }
            }
        }
        CancelInvoke("UpdateTimer");
        face.sprite = faces[0];
        for (int y = 0; y < 9; y++) {
            for (int x = 0; x < 9; x++) {
                grid[y, x].sprite = places[9];
            }
        }
        GenerateBoardNumbers();
        complete = false;
        timing = false;
        time = 0;
        bombCount = 10;
        RenderNumber(timer, time);
        RenderNumber(bombCounter, bombCount);
        aud.clip = sounds[0];
        aud.Play();
    }

    // Reveals a non-bomb square
    private void RevealSquare(int yPos, int xPos) {
        // Check if square untouched
        if (grid[yPos,xPos].sprite == places[9]) {
            // Reveal middle square
            grid[yPos, xPos].sprite = places[board[yPos, xPos]];
            // Check if square is empty
            if (board[yPos, xPos] == 0) {
                // Reveal surrounding squares
                for (int y = (yPos - 1); y < (yPos + 2); y++) {
                    for (int x = (xPos - 1); x < (xPos + 2); x++) {
                        if (!(y == yPos && x == xPos) && (y >= 0 && y < 9 && x >= 0 && x < 9)) {
                            RevealSquare(y, x);
                        }
                    }
                }
            }
        }
    }

    private void RevealBoard() {
        for (int y = 0; y < 9; y++) {
            for (int x = 0; x < 9; x++) {
                // Check if untouched and bomb
                if (grid[y, x].sprite == places[9] && board[y, x] == -1) {
                    grid[y, x].sprite = places[11];
                }
                // Check if flag
                if (grid[y, x].sprite == places[10]) {
                    // Check if flag correct
                    if (board[y, x] != -1) {
                        // Mark flag as wrong
                        grid[y, x].sprite = places[13];
                    }
                }
            }
        }            
    }

    private void CheckBoard() {
        bool untouchedExists = false;
        bool wrongFlag = false;
        for (int y = 0; y < 9; y++) {
            for (int x = 0; x < 9; x++) {
                // Check for untouched square
                if (grid[y, x].sprite == places[9]) {
                    untouchedExists = true;
                }
                // Check for unflagged bomb
                if (board[y, x] == -1 && grid[y, x].sprite != places[10]) {
                    wrongFlag = true;
                }
                // Check for wrong flag
                if (board[y, x] != -1 && grid[y, x].sprite == places[10]) {
                    wrongFlag = true;
                }
            }
        }
        if (!untouchedExists) {
            // Board full
            if (wrongFlag) {
                // Lose
                RevealBoard();
                EndGame(false);
            } else {
                // Win
                RevealBoard();
                EndGame(true);
            }
        }
    }

    private void EndGame(bool win) {
        if (win) {
            face.sprite = faces[1];
            aud.clip = sounds[2];
            aud.Play();
        } else {
            face.sprite = faces[2];
            aud.clip = sounds[1];
            aud.Play();
        }
        complete = true;
        timing = false;
        CancelInvoke("UpdateTimer");
    }

    private void UpdateTimer() {
        time++;
        RenderNumber(timer, time);
    }

    private void RenderNumber(Image[] imgs, int num) {
        num = Mathf.Clamp(num, -99, 999);
        string s = num.ToString();
        // Format negatives to be -0 instead of 0-
        if (s.Length == 2 && s[0] == '-') {
            s = "-0" + s[1];
        }
        if (s.Length == 3) {
            if (s[0] == '-') {
                imgs[0].sprite = digits[10];
            } else {
                imgs[0].sprite = digits[int.Parse(s[0].ToString())];
            }
        } else {
            imgs[0].sprite = digits[0];
        }
        if (s.Length >= 2) {
            if (s[s.Length - 2] == '-') {
                imgs[1].sprite = digits[10];
            } else {
                imgs[1].sprite = digits[int.Parse(s[s.Length - 2].ToString())];
            }
        } else {
            imgs[1].sprite = digits[0];
        }
        imgs[2].sprite = digits[int.Parse(s[s.Length - 1].ToString())];
    }

    public void GridPress(int y, int x, bool isLeft) {
        if (!complete) {
            if (isLeft) {
                // Start timer
                if (!timing) {
                    timing = true;
                    InvokeRepeating("UpdateTimer", 1, 1);
                }
                // Check if untouched
                if (grid[y, x].sprite == places[9]) {
                    // Check if bomb
                    if (board[y, x] == -1) {
                        // Lose
                        RevealBoard();
                        grid[y, x].sprite = places[12];
                        EndGame(false);
                    } else {
                        RevealSquare(y, x);
                    }
                }
            } else {
                // Check if untouched or flag
                if (grid[y, x].sprite == places[9]) {
                    // Place flag
                    grid[y, x].sprite = places[10];
                    bombCount--;
                    RenderNumber(bombCounter, bombCount);
                } else if (grid[y, x].sprite == places[10]) {
                    // Remove flag
                    grid[y, x].sprite = places[9];
                    bombCount++;
                    RenderNumber(bombCounter, bombCount);
                }
            }
            // Check if complete
            CheckBoard();
        }
    }

    public void FacePress() {
        NewBoard();
    }
}