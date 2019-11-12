USE [SU_Casino]
GO

--This is only needed to create the same text that are stored in production, before
DELETE from [dbo].[InfoText]
GO

INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (1, N'Let''s play a game of roulette! Place a bet of 500 credits on either Black or Red!', N'playRouletteInfo')
GO
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (2, N'Let''s play Card Match! Pick a card by selecting one of the two decks and see if it matches the one in the middle. One deck costs more but you can also win more. Try each of the two decks at least once and play as you would in a real casino. Keep an eye on your credit score. Best of luck!', N'playCardInfo')
GO
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (3, N'Let''s play slot machines! Place your bet and spin the dials by pressing the button! Best of luck!', N'playSlotInfo')
GO
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (4, N'Thank you very much for playing!<br/><br/>Your results will help researchers understand why some people have a hard time controlling their gambling, which can have very negative consequences. If you believe that you may have a gambling problem, or if someone close to you is concerned about your gambling, please seek help (the sooner, the better). <br/><br/>Debriefing: In order to study how gambling behaviors are learned and extinguished in a naturalistic gambling setting, key gambling parameters in this study such as win probabilities and price money, were fixed and randomized across participants. Because of this, all study participants receive the same AMT reimbursement. Any gambling strategies “learned” during this study will not work in real-world gambling. And remember: in real-life, the house always wins. <br/><br/>Remember to copy the survey code below (write it down to be sure), then return to the browser window you left that is still at the AMT platform and enter the code (exactly as it appears below) in the field there to submit your work and be eligible for reimbursement. Do not close this window until you have submitted and thereby completed the task in the other window on the AMT side. If you have any questions about this study, please contact principal investigator professor Per Carlbring (per.carlbring@psychology.su.se). <br/><br/>Thank you again<br/>Stockholm University', N'endPage')
GO
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (5, N'Before we continue to the games, we would like you to answer some quick questions about your gambling habits. <br/><br/>After that, you will automatically be taken through a series of different games, playing several trials of each. You will not be able to influence which games you play, or the order of them, or the number of trials. The outcome of each bet, and your final credit sum at the end of the session, will however depend on how you play the games – just like in a real casino. When you win, in addition to the amount won, <u>you also win back your bet</u>. At the end of each game, you will be asked a question on how the game worked, so <u>please pay attention while playing!</u> <br/><br/>Reminder: this is a scientific experiment, not a real, working casino. That said, please play the games as you would in a real casino!', N'startPage')
GO
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (6, N'Let''s play Card Match! Pick a card from one of the two decks and see if it matches the one in the middle!', N'playCardWinFreezeInfo')
GO
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (7, N'Let''s play Card Match! Pick a card by selecting one of the two decks -- make sure to try each of them at least once! -- and see if it matches the one in the middle. Your bet is placed the moment you select your deck and your credit score (bottom right) is updated depending on how it goes. Best of luck!', N'playCardNoSaldoInfo')
GO

 