const employees =  document.getElementById('employees');
const issuePoints = document.getElementById('issuePoints');
const generateReport = document.getElementById('generateReport');

employees.addEventListener('mousedown', render, false);
issuePoints.addEventListener('mousedown', render, false);
generateReport.addEventListener('mousedown', render, false);

function render(e){
    if(e.srcElement.id === 'employees'){
        issuePoints.classList.remove('active-nav');
        generateReport.classList.remove('active-nav');
        employees.classList.add('active-nav');
    }
    else if( e.srcElement.id === 'issuePoints'){
        employees.classList.remove('active-nav');
        generateReport.classList.remove('active-nav');
        issuePoints.classList.add('active-nav');
    }
    else if( e.srcElement.id === 'generateReport'){
        issuePoints.classList.remove('active-nav');
        employees.classList.remove('active-nav');
        generateReport.classList.add('active-nav');
    }
}
