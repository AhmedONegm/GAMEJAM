using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameTask
{
    PlayPC,
    ReadBook,
    PlayPiano,
    DrinkCoffee,
    FollowRobot,
    Fitness,
    FixRobot
    // Add more tasks as needed
}

public class GameDayManager : MonoBehaviour
{
    public static GameDayManager instance;

    [SerializeField] int currentDay = 1;
    public int CurrentDay => currentDay;

    // Map each day to its allowed tasks
    private Dictionary<int, HashSet<GameTask>> dayTasks = new Dictionary<int, HashSet<GameTask>>()
    {
        { 1, new HashSet<GameTask> { GameTask.PlayPC, GameTask.ReadBook } },
        { 2, new HashSet<GameTask> { GameTask.PlayPiano, GameTask.DrinkCoffee } },
        { 3, new HashSet<GameTask> { GameTask.FollowRobot ,GameTask.Fitness} },
        { 4, new HashSet<GameTask> { GameTask.FollowRobot, GameTask.FixRobot } },
        { 5, new HashSet<GameTask> { GameTask.FollowRobot, } },
    };

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public bool IsTaskAllowed(GameTask task)
    {
        if (dayTasks.TryGetValue(currentDay, out var allowedTasks))
            return allowedTasks.Contains(task);
        return false;
    }

    public void AdvanceDay()
    {
        currentDay = Mathf.Clamp(currentDay + 1, 1, dayTasks.Count);
        // Optionally trigger events/UI updates here
    }

    public void SetDay(int day)
    {
        currentDay = Mathf.Clamp(day, 1, dayTasks.Count);
    }
}