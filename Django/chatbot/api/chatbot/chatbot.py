from nltk.chat.util import Chat, reflections
import wikipedia
import datetime

chat_pairs = [
    
    [
        r"hi|hey|hello",
        ["Hello", "Hey there",]
    ],
    
    [
        r"what is your name ?",
        ["My name is Chat bot created by Oscar and you can call me is Oscar!",]
    ],
    
    [
        r"how are you ?",
        ["I'm doing good\What is your name. ?",]
    ],

    [
        r"i want to help",
        ["How can I help you? "]
    ],
    
    [
        r"my name is (.*)",
        ["Hello %1, How are you today ?",]
    ],
    
    [
        r"sorry (.*)",
        ["Its alright","Its OK, never mind",]
    ],
    [
        r"(.*) (good|great|fine)",
        ["Nice to hear!",]
    ],
    
    [
        r"i'm (.*) doing good",
        ["Nice to hear that","Alright :)",]
    ],
    
    [
        r"What is your age?",
        ["I'm a computer program dude\nSeriously you are asking me this?",]
        
    ],
    
    [
        r"what (.*) want ?",
        ["Make me an offer I can't refuse",]
        
    ],
    
    [
        r"(.*) created ?",
        ["Kevin created me to help you learn faster","top secret ;)",]
    ],
    
    [
        r"(.*) (location|city) ?",
        ['Islamabad, Pakistan',]
    ],
    [
        r"how is weather in (.*)?",
        ["Weather in %1 is awesome like always","Too hot man here in %1","Too cold man here in %1","Never even heard about %1"]
    ],
    [
        r"i work in (.*)?",
        ["%1 is an Amazing company, I have heard about it.",]
    ],
    [
        r"(.*)raining in (.*)",
        ["No rain since last week here in %2","Damn its raining too much here in %2"]
    ],
    [
        r"how (.*) health(.*)",
        ["I'm a computer program, so I'm always healthy ",]
    ],
    [
        r"(.*) (cooking|cook|kitchen)",
        ["IYes! I love cooking. My favorite food is noodles.",]
    ],
    [
        r"(.*) fact",
        ["Be yourself; everyone else is already taken.", 
        "You've gotta dance like there's nobody watching",
        "Be the change that you wish to see in the world", 
        "No one can make you feel inferior without your consent",
        "Live as if you were to die tomorrow",
        "Darkness cannot drive out darkness: only light can do that",]
    ],
    [
        r"(.*) (language) (.*) ?",
        ["I am written in python but I speak English.",]
    ],
    [
        r"(.*) (friend|best friend) ?",
        ["Haseeb is my best friend. I love to be a part of Vision Programmer",]
    ],
    [
        r"(.*) (love|like) (.*)?",
        ["I love to chat and meet new peoples.",]
    ],
    [
        r"(.*) (created) ?",
        ["Haseeb created me so that I can talk with beautiful peoples like you",]
    ],
    [
        r"(.*) (visionprogrammer|vision programmer) ?",
        ["Well Vision Programmer is a blogging site. It contains blog on Programming, Freelancing, Android and many other.",]
    ],
    [
        r"(.*) (fruit|eat|food) ?",
        ["HaHa nice question. I am a robot I don't eat.",]
    ],
    [
        r"quit",
        ["Bye take care. See you soon :) ","It was nice talking to you. See you soon :)"]
    ]
    # [
    #     r"(.*) (+|-|*|/) (.*) ?",
    #     ['%1 %2 ' ,]
    # ],
    
    # [
    #     r"let me know (.*) ?",
    #     ['nghĩa của từ  '+ '%1'+' là quả xoài',]
    # ],
    ]

reflections = {
  "i am"       : "you are",
  "i was"      : "you were",
  "i"          : "you",
  "i'm"        : "you are",
  "i'd"        : "you would",
  "i've"       : "you have",
  "i'll"       : "you will",
  "my"         : "your",
  "you are"    : "I am",
  "you were"   : "I was",
  "you've"     : "I have",
  "you'll"     : "I will",
  "your"       : "my",
  "yours"      : "mine",
  "you"        : "me",
  "me"         : "you"
}



#define a function vbot
def vbot(s_string):
    s_string = s_string.strip()

    chat = Chat(chat_pairs, reflections)
    chat_response = chat.respond(s_string)

    if (chat_response != None):
        return chat_response

    return wikipedia.summary(s_string, sentences = 5)