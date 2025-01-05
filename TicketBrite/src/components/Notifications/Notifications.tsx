import { toast, ToastContentProps } from "react-toastify";

interface Props {
    text: string;
};

interface WarningProp {
    text: string;
    onConfirm: () => void;
    onCancel: () => void;
};

export function SuccessNotification({ text }: Props) {
    if (typeof text === 'string') {
        return toast.success(text, {
            position: "top-right",
            autoClose: 2500,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            toastId: 1
        });
    } else {
        return toast.error("Actie is succesvol geslaagd, maar er is iets misgegaan met het weergeven van de melding!");
    }
}

export function ErrorNotification({ text }: Props) {
    if (typeof text === 'string') {
        return toast.error(text, {
            position: "top-right",
            autoClose: 2500,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            toastId: 2
        });
    } else {
        return toast.error("Actie is succesvol geslaagd, maar er is iets misgegaan met het weergeven van de melding!");
    }
}

export function WarningNotification({ text, onConfirm, onCancel }: WarningProp) {
    
    if (typeof text === 'string') {
        return toast(
            (props: ToastContentProps) => (
                <div>
                    <p>{text}</p>
                    <div style={{ display: "flex", justifyContent: "space-between", marginTop: "10px" }}>
                        <button
                            onClick={() => {
                                onConfirm();

                                // Controleer of props.closeToast een functie is voordat we het aanroepen
                                if (typeof props.closeToast === 'function') {
                                    props.closeToast(); // Sluit de melding als het een functie is
                                } else {
                                    console.error('closeToast is geen geldige functie');
                                }
                            }}
                            className="btn btn-success"
                            data-test="confirm-delete-item"
                        >
                            Ja
                        </button>
                        <button
                            onClick={() => {
                                onCancel();
                                props.closeToast?.(); // Sluit de melding
                            }}
                            className="btn btn-danger"
                             data-test="cancel-delete-item"
                        >
                            Nee
                        </button>
                    </div>
                </div>
            ),
            {
                autoClose: false,
                closeOnClick: false,
                position: "top-right",
                toastId: 3
            }
        );
    } else {
        return toast.error("Actie is succesvol geslaagd, maar er is iets misgegaan met het weergeven van de melding!");
    }
}
