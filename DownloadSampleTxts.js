(function(document,s){
	for (let i = 0; i < 5; i++) {
		const inSampleEle  = document.querySelector(`#task-statement .lang-ja #pre-sample${i * 2}`);
		const outSampleEle  = document.querySelector(`#task-statement .lang-ja #pre-sample${i * 2 + 1}`);
		if(inSampleEle == null) break;

		downloadTxtFile(`${i + 1}In.txt`, inSampleEle.textContent);
		downloadTxtFile(`${i + 1}Out.txt`, outSampleEle.textContent);
	}

	function downloadTxtFile(filename, textContent) {
		const blob = new Blob([textContent],{type:"text/plain"});
		const link = document.createElement('a');
		link.href = URL.createObjectURL(blob);
		link.download = filename;
		link.click();
	}
})(document);