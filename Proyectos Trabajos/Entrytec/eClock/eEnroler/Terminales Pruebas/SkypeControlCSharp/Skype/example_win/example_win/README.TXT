following special commands are recognized and handled by this client
  #quit
  #exit
    terminate client
  #dbgon
    turn on debug printing of windows messages
  #dbgoff
    turn off debug printing of windows messages
  #connect
    discover skype api
  #disconnect
    terminate connection to skype api
all other commands are sent "as is" directly to Skype
(no UTF-8 translation is done at present)
