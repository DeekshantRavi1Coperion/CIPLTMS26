function inNumberKey(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
        
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        return true;
    }       
}

function inNumberKeyWithDecimal(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
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