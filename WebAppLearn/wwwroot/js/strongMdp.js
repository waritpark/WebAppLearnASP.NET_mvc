// vérifier la complexité du mdp

function logKey() {
    var mdp = document.getElementById('password').value;
    var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
    var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
    var okRegex = new RegExp("(?=.{6,}).*", "g");
    var verifmdp = document.getElementById('passwordStrength');

    if (okRegex.test(mdp) === false) {
        verifmdp.classList.remove('alert-secondary','alert-danger','alert-success','alert-info');
        verifmdp.classList.add('alert-danger');
        verifmdp.innerHTML='Le mot de passe doit contenir 6 caractères minimum.';
    } else if (strongRegex.test(mdp)) {
        verifmdp.classList.remove('alert-secondary','alert-danger','alert-success','alert-info');
        verifmdp.classList.add('alert-success');
        verifmdp.innerHTML='Fiabilité du mot de passe : Excellent !';
    } else if (mediumRegex.test(mdp)) {
        verifmdp.classList.remove('alert-secondary','alert-danger','alert-success','alert-info');
        verifmdp.classList.add('alert-info');
        verifmdp.innerHTML='Fiabilité du mot de passe : moyenne.';
    } else {
        verifmdp.classList.remove('alert-secondary','alert-danger','alert-success','alert-info');
        verifmdp.classList.add('alert-danger');
        verifmdp.innerHTML='Fiabilité du mot de passe : mauvaise.';
    }
    return true;
};
