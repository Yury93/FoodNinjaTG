
mergeInto(LibraryManager.library, {
   
    CloseFrameGame: function() {  
        console.log('close frame WINDOW')
                  var message = JSON.stringify({type:'MyFunctionCall'});  
        window.top.postMessage(message, '*');  
    },
       AddScore: function(score) {  
                console.log('add score')
                  var message = JSON.stringify({type:'AddScore',data:score});  
        window.top.postMessage(message, '*');  
    },
});