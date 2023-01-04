using GuessingGame.models;
using GuessingGame.Models;
using GuessingGame.Repositories;

namespace GuessingGame.Proxies;

public class ScoreboardLineProxy: ScoreboardLine
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGuessingGameRepository _guessingGameRepository;

    public ScoreboardLineProxy(IPlayerRepository playerRepository, IGuessingGameRepository guessingGameRepository)
    {
        _playerRepository = playerRepository;
        _guessingGameRepository = guessingGameRepository;
    }

    public override Player Player
    {
        get
        {
            if (base.Player == null)
            {
                base.Player = _playerRepository.GetPlayerByScoreboardLineId(Id);
            }

            return base.Player;
        }
        
        set => base.Player = value;
    }

    public override GuessingGameModel? Game
    {
        get
        {
            if (base.Game == null)
            {
                base.Game = _guessingGameRepository.GetGameByScoreboardLineId(Id);
            }
            

            return base.Game;
        }
    }
}