using DecisionMakingServer.APIModels;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DecisionMakingServer.Tests;

public class ControllerTest
{
    private readonly RankingRepository _rankingRepository = new();
    private readonly AnswerRepository _answerRepository = new();
    
    public void AddRankingTest(Ranking ranking)
    {
        int id = _rankingRepository.AddRanking(ranking);
        Console.WriteLine($"Ranking add: {id}");
    }

    public Ranking? GetRankingTest(int id)
    {
        Ranking? r = _rankingRepository.GetRankingWithData(id);
        Console.WriteLine("Ranking get:");
        Console.WriteLine(r);
        return r;
    }

    public void AddAnswersTest(RankingPostDTO postDto)
    {
        var answers = postDto.Answers.Select(a => a.ToAnswer(1)).ToList();
        answers.ForEach(a => { a.RankingId = postDto.RankingId;});
        Status s = _answerRepository.AddAnswers(answers);
        Console.WriteLine($"Add answers: {s}");
    }
}