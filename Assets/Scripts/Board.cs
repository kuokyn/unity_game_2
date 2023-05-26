using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    // блок доски 9 на 9
    int[,] solvedGrid = new int[9,9];
    string debug;

    int[,] riddleGrid = new int[9, 9];
    int piecesToErase;

    public Transform A1, A2, A3, B1, B2, B3, C1, C2, C3;
    public GameObject buttonPrefab;

    // difficulty level
    public enum Difficulties
    {
        DEBUG, EASY, MEDIUM, HARD, EXPERT
    }

    public Difficulties difficulty;

    List<NumberField> fieldList = new List<NumberField>();
    int maxHints;

    public GameObject win;
    public GameObject notDone;
    public GameObject noMoreHints;
    public GameObject restartButton;
	public GameObject backToMenuButton;

	// Start is called before the first frame update
	void Start()
    {
		win.SetActive(false);
		notDone.SetActive(false);
		noMoreHints.SetActive(false);
		restartButton.SetActive(false);
		backToMenuButton.SetActive(false);
		difficulty = (Board.Difficulties) ChooseDifficulty.difficulty;
        InitGrid(ref solvedGrid);
		DebugGrid(ref solvedGrid);
        ShuffleGrid(ref solvedGrid, 97);
        CreateRiddleGrid();
        CreateButtons();
	}

    void InitGrid(ref int[,] grid)
    {
        for(int i = 0; i< 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                grid[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
			}
        }
	}

    void DebugGrid(ref int[,] grid)
    {
        debug = "";
        int sep = 0;
		for (int i = 0; i < 9; i++)
		{
            debug += "|";
			for (int j = 0; j < 9; j++)
			{
                debug += grid[i, j].ToString();
                sep = j % 3;
                if (sep == 2)
                {
                    debug += "|";
                }
			}
			debug += "\n";
		}
        print(debug);
	}

    void ShuffleGrid(ref int[,] grid, int shuffleAmount)
    {
        for(int i = 0; i<shuffleAmount; i++)
        {
            int value1 = Random.Range(1, 10);
			int value2 = Random.Range(1, 10);
            MixTwoGridCells(ref grid, value1, value2);
		}
        // DebugGrid(ref grid);
	}

    void MixTwoGridCells(ref int[,] grid, int value1, int value2)
    {
        int x1 = 0;
        int x2 = 0;
        int y1 = 0;
        int y2 = 0;

        for(int i = 0;i < 9; i+=3)
        {
            for(int k = 0; k < 9; k += 3)
            {
                for(int j = 0; j < 3; j++)
                {
                    for(int l = 0; l < 3; l++)
                    {
                        if (grid[i+j, k+l] == value1)
                        {
                            x1 = i + j;
                            y1 = k + l;
                        }

						if (grid[i + j, k + l] == value2)
						{
							x2 = i + j;
							y2 = k + l;
						}
					}
                }
                grid[x1, y1] = value2;
                grid[x2, y2] = value1;
            }
        }
    }

    void CreateRiddleGrid( )
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                riddleGrid[i, j] = solvedGrid[i, j];

			}
            
        }

        SetDifficulty();

        for (int i = 0; i < piecesToErase; i++)
        {
            int x1 = Random.Range(0, 9);
			int y1 = Random.Range(0, 9);

            // выбираем какие поля стереть
            while (riddleGrid[x1, y1] == 0)
            {
				x1 = Random.Range(0, 9);
				y1 = Random.Range(0, 9);
			}
            riddleGrid[x1, y1] = 0;
		}
		// DebugGrid(ref riddleGrid);
	}

    void CreateButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject newButton = Instantiate(buttonPrefab);

                // setting values
                NumberField numField = newButton.GetComponent<NumberField>();
                numField.SetValues(i, j, riddleGrid[i,j], i + "," + j, this);
                newButton.name = i + "," + j;

                if (riddleGrid[i,j] == 0)
                {
                    fieldList.Add(numField);
                }

                // parenting buttons
                // A1 - левый верхний угол
                if (i < 3 && j < 3)
                {
                    newButton.transform.SetParent(A1, false);
                }
				// A2
				if (i < 3 && j > 2 && j < 6)
				{
					newButton.transform.SetParent(A2, false);
				}
				// A3
				if (i < 3 && j > 5)
				{
					newButton.transform.SetParent(A3, false);
				}

				// B1
				if (i > 2 && i < 6 && j < 3)
				{
					newButton.transform.SetParent(B1, false);
				}
				// B2
				if (i > 2 && i < 6 && j > 2 && j < 6)
				{
					newButton.transform.SetParent(B2, false);
				}
				// B3
				if (i > 2 &&  i < 6 && j > 5)
				{
					newButton.transform.SetParent(B3, false);
				}

				// C1
				if (i > 5 && j < 3)
				{
					newButton.transform.SetParent(C1, false);
				}
				// C2
				if (i > 5 && j > 2 && j < 6)
				{
					newButton.transform.SetParent(C2, false);
				}
				// C3
				if (i > 5 && j > 5)
				{
					newButton.transform.SetParent(C3, false);
				}
			}
        }
    }

    public void SetInputRiddleGrid(int x, int y, int value)
    {
        riddleGrid[x, y] = value;
    }

    void SetDifficulty()
    {
        switch (difficulty)
        {
            case Difficulties.DEBUG:
                piecesToErase = 5;
                maxHints = 3;
                break;
			case Difficulties.EASY:
				piecesToErase = 27;
				maxHints = 3;
				break;
			case Difficulties.MEDIUM:
				piecesToErase = 36;
				maxHints = 5;
				break;
			case Difficulties.HARD:
				piecesToErase = 45;
				maxHints = 7;
				break;
			case Difficulties.EXPERT:
				piecesToErase = 54;
				maxHints = 9;
				break;
		}
    }

    public void CheckComplete()
    {
		restartButton.SetActive(true);
		backToMenuButton.SetActive(true);
		if (CheckIfWon())
        {
			win.SetActive(true);
			// print("you won");
		}
        else
        {
			notDone.SetActive(true);
			// print("you haven't finished your field");
		}
    }

    bool CheckIfWon()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (riddleGrid[i,j] != solvedGrid[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void ShowHint()
    {
        if(fieldList.Count > 0 && maxHints > 0)
        {
            int randIndex = Random.Range(0, fieldList.Count);
            maxHints--;
            riddleGrid[fieldList[randIndex].GetX(), fieldList[randIndex].GetY()] = solvedGrid[fieldList[randIndex].GetX(), fieldList[randIndex].GetY()];
            fieldList[randIndex].SetHint(riddleGrid[fieldList[randIndex].GetX(), fieldList[randIndex].GetY()]);
            fieldList.RemoveAt(randIndex);
        }
        else
        {
            noMoreHints.SetActive(true);
			restartButton.SetActive(true);
			backToMenuButton.SetActive(true);
		}
    }
}
