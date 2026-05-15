// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function productAanWinkelwagen(id, name, price, description) { 

    let winkelwagen = JSON.parse(sessionStorage.getItem("winkelwagen")) || []

    winkelwagen.push({
        id: id,
        naam: name,
        prijs: price,
        beschrijving: description
    });

    sessionStorage.setItem("winkelwagen", JSON.stringify(winkelwagen));
}

function laadWinkelwagen() {
    const cart = document.getElementById("cart");
    const winkelwagen = JSON.parse(sessionStorage.getItem("winkelwagen")) || [];

    if (winkelwagen.length === 0) {
        cart.innerText = "winkelwagen is leeg";
        return;
    }

    cart.innerHTML = winkelwagen
        .map(p => `${p.naam} - €${p.prijs}`)
        .join("<br>");
    
}