global = {
    hasBeneficiary: false,
    specifyShares: false,
    AssetsBeneficiary: false,
    hasWitness: false,
    AddWitness: false,
    hasSpouse: false
}


function setBeneficiary(inHasBeneficiary) {
    //console.log("hello");
    global.hasBeneficiary = inHasBeneficiary;
    if (global.hasBeneficiary) {
        document.getElementById("bDetails").style.display = "block";
    }
    else {
        document.getElementById("bDetails").style.display = "none";
    }
}
function setWitness(inHasWitness) {
    global.hasWitness = inHasWitness
    if (global.hasWitness) {
        document.getElementById("wDetails").style.display = "block";
        document.getElementById("wNDetails").style.display = "none";
    }
    else {
        document.getElementById("wDetails").style.display = "none";
        document.getElementById("wNDetails").style.display = "block";
    }
}
function toggleShares() {
    //console.log("hello");
    global.specifyShares = !global.specifyShares;
    if (global.specifyShares) {
        document.getElementById("specifyShareContainer").style.display = "block";

    }
    else {
        document.getElementById("specifyShareContainer").style.display = "none";
    }
}
function toggleWitness() {
    //console.log("hello");
    global.AddWitness = !global.AddWitness;
    if (global.AddWitness) {
        document.getElementById("wForm").style.display = "block";

    }
    else {
        document.getElementById("wForm").style.display = "none";
    }
}
function toggleSpouse() {
    //console.log("hello");
    global.hasSpouse = !global.hasSpouse;
    if (global.hasSpouse) {
        document.getElementById("sDetails").style.display = "block";

    }
    else {
        document.getElementById("sDetails").style.display = "none";
    }
}
function toggleBeneficiary(AssetsBeneficiaryL) {
    global.AssetsBeneficiary = AssetsBeneficiaryL
    if (global.AssetsBeneficiary) {
        document.getElementById("bDetails").style.display = "block";

    }
    else {
        document.getElementById("bDetails").style.display = "none";
    }
}

function toggleAsset() {
    console.log("toggleAsset==>Start");
//    document.getElementById("cash_type").style.display = "none";
//    document.getElementById("real_estate_type").style.display = "none";
///*    document.getElementById("bank_type").style.display = "none";*/
//    document.getElementById("other_asset_type").style.display = "none";
    var select = document.getElementById("gift_type");
    var value = select.options[select.selectedIndex].value;
    console.log("toggleAsset==>", value);
    if (value == "Cash") {
        /*document.getElementById("cash_type").style.display = "block";*/
        document.getElementById("specificid").innerText = "Amount to give";
    }
    //}
    else if (value == "Real Estate") {
        document.getElementById("specificid").innerText = "What is your real estate address";

    }
    else {
        document.getElementById("specificid").innerText = "What are the other assets you would like to specify";
    }
}
function toggleStatus() {
    console.log("toggleStatus==>Start");
    document.getElementById("MaritalForm").style.display = "none";
    var select = document.getElementById("MaritalStatusList");
    var value = select.options[select.selectedIndex].value;
    console.log("toggleStatus==>", value);
    if (value == "Married") {
        document.getElementById("MaritalForm").style.display = "block";

    }
    else {
        document.getElementById("MaritalForm").style.display = "none";

    }
}
