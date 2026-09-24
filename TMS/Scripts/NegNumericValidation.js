function negNumberKeyWithDecimal(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;   
    if (charCode == 45) {              
        if (txt.value.indexOf('-') === -1) {
            return true;
        }
        else {
            return false;
        }
    }
    if (charCode == 46) {
        if (txt.value.indexOf('.') === -1) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
    }
    return true;
}
