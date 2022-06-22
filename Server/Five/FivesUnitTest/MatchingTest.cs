﻿using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class MatchingTest
    {
        Matching matching;
        [SetUp]
        public void SetUp()
        {
            matching = new Matching();
        }
        [Test]
        public void testMatching()
        {
            Assert.AreEqual(0,matching.GameCount);
        }
        [Test]
        public void testMatchOnePlayer()
        {
            var player = new LogPlayer();
            matching.Match(player);
            Assert.AreEqual("Match ", player.log);
            Assert.AreNotEqual(0, player.GameId);
            Assert.AreEqual(1, matching.GameCount);
            Assert.AreEqual(1, matching.GetGame(player.GameId).PlayerCount);
        }
        [Test]
        public void testMatchTwoPlayer()
        {
            var players = new LogPlayer[2];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogPlayer();
            }
            for (int i = 0; i < players.Length; i++)
            {
                matching.Match(players[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                Assert.AreEqual("Match Start ", players[i].log);
            }
        }
        [Test]
        public void testMatchMutltyPlayer()
        {
            var players = new LogPlayer[4];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogPlayer();
            }
            for (int i = 0; i < players.Length; i++)
            {
                matching.Match(players[i]);
            }
            Assert.AreNotEqual(players[0].GameId, players[2].GameId);
        }
        [Test]
        public void testGetGame()
        {
            Assert.AreEqual(null, matching.GetGame(0));
        }
        [Test]
        public void testCancel()
        {
            var player = new LogPlayer();
            matching.Match(player);
            int id = player.GameId;
            matching.Cancel(player);
            Assert.AreEqual(0, matching.GetGame(id).PlayerCount);
            Assert.AreEqual("Match CancelMatch ", player.log);
        }
    }
}
