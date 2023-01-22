export default function Betalen() {
	<body>
		U moet undefined euro betalen.
		<br />
		Het account NL55ABNA5660751954 heeft oneindig veel geld.
		<br />
		Het account NL02INGB8635612388 heeft in 50% van de gevallen genoeg geld.
		<br />
		Alle andere accounts hebben niet genoeg geld.
		<br />
		<form method="post" action="undefined">
			Bankrekeningnummer: <input name="account"></input>
			<input type="hidden" id="succes" name="succes" value="false"></input>
			<input type="hidden" name="reference" value="undefined"></input>
		</form>
	</body>
}