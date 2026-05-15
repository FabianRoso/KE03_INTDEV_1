// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function productAanWinkelwagen(id, name, price, description) { 

    let winkelwagen = JSON.parse(sessionStorage.getItem("winkelwagen")) || []

    price = parseFloat(price.replace(",", "."));

    winkelwagen.push({
        id: id,
        name: name,
        price: price,
        description: description
    });

    sessionStorage.setItem("winkelwagen", JSON.stringify(winkelwagen));
}

function laadWinkelwagen() {
    const cartDiv = document.getElementById("cart");
    const winkelwagen = JSON.parse(sessionStorage.getItem("winkelwagen")) || [];

    if (winkelwagen.length === 0) {
        cartDiv.innerText = "winkelwagen is leeg";
        return;
    }

    let html = "";

    winkelwagen.forEach(function (product, index) {
        html += `
            <div>
                <h3>${product.name}</h3>
                <p>Prijs: €${product.price}</p>
                <p>${product.description}</p>

                <button onclick="verwijderUitWinkelwagen(${index})">
                    Verwijder
                </button>
            </div>
            <hr>
        `;
    });

    cartDiv.innerHTML = html;
}

function verwijderUitWinkelwagen(index) {
    let cart = JSON.parse(sessionStorage.getItem("winkelwagen")) || [];

    cart.splice(index, 1);

    sessionStorage.setItem("winkelwagen", JSON.stringify(cart));

    laadWinkelwagen();
}

function krijgWinkelwagen() {
    let cart = sessionStorage.getItem("winkelwagen") || "[]";

    document.getElementById("cartData").value = cart;
}
