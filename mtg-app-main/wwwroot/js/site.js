"use strict";

document.addEventListener("DOMContentLoaded", init);

function init(){
    document.querySelector('#searchInput').addEventListener('input', filter);
}

function addCardToLikes(obj){
    let currentLikes = localStorage.getItem('Likes');
    if(!currentLikes){
        currentLikes = [];
    }else{
        currentLikes = JSON.parse(currentLikes);
    }
    currentLikes.push(obj);
    localStorage.setItem('Likes', JSON.stringify(currentLikes));
    window.alert("Succesfully added "+ obj.name + " to your likes!");
}

function filter(){
    let input = document.querySelector('#searchInput').value;
    let filter = input.toUpperCase();
    let cards = document.querySelectorAll('.card-name');
    let cardnames = []; 
    for (let i = 0; i < cards.length; i++){
        cardnames.push(cards[i].childNodes[0].nodeValue);
    }

    let filteredNames = [];
    for (let i = 0; i < cardnames.length; i++) {
        if (cardnames[i].toUpperCase().indexOf(filter) > -1) {
            filteredNames.push(cardnames[i]);
        } else {
            let wholeCard = cards[i].parentElement;
            let cardToHide = wholeCard.parentElement;
            cardToHide.style.display = "none";
        }
    }
    

}