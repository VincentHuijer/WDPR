import ArticlePreview from "../Components/ArticlePreview";
import Kaart from "../Components/Kaart";

export default function OverOns() {
  return (
    <div className="w-full mt-32">
      <ArticlePreview
        text={
          "Laaktheater probeert kunst persoonlijk te maken. Dat betekent dat we kunst op zo veel verschillende manieren brengen dat er voor iedereen een mogelijkheid is het zich eigen te maken en te beleven. Dat kunst niet eng is of niet voor jou, maar dat kunst echt voor en van iedereen is. Als je maar een manier vindt of krijgt aangeboden die bij je past. "
        }
        overonspagina={true}
      />
      <Kaart />
    </div>
  );
}
