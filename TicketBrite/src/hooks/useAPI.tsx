import { ErrorNotification } from "../components/Notifications/Notifications";
import { ApiResponse } from "../Types";

export async function APICall(url: string, type: string, value: any){
        try {
            // Verzend het formulier naar het endpoint
            const res = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(
                    value
                )
            });

            const data: ApiResponse<string> = await res.json();
            return data;
        } catch (error) {
            ErrorNotification({text: "Gegevens kunnen niet worden opgeslagen!"});
        }
}