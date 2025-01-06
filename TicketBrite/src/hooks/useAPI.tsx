import { ErrorNotification } from "../components/Notifications/Notifications";
import { ApiResponse } from "../Types";

export async function APICall(url: string, type: string, value: unknown, authToken: string){
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

            const data = await res.json() as ApiResponse<string>;
            return data;
        } catch (error) {
            ErrorNotification({text: "Gegevens kunnen niet worden opgeslagen!"});
        }
}