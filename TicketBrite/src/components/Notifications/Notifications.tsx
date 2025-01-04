import { toast, ToastContentProps } from "react-toastify";

type Props = {
    text: string;
};

type WarningProp = {
    text: string;
    onConfirm: () => void;
    onCancel: () => void;
};

export function SuccessNotification({ text }: Props) {
    return toast.success(text, {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
    });
}

export function ErrorNotification({ text }: Props) {
    return toast.error(text, {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
    });
}

export function WarningNotification({ text, onConfirm, onCancel }: WarningProp) {
    return toast(
        (props: ToastContentProps) => (
            <div>
                <p>{text}</p>
                <div style={{ display: "flex", justifyContent: "space-between", marginTop: "10px" }}>
                    <button
                        onClick={() => {
                            onConfirm();
                            props.closeToast?.(); // Sluit de melding
                        }}
                        style={{
                            marginRight: "10px",
                            backgroundColor: "green",
                            color: "white",
                            border: "none",
                            padding: "5px",
                            cursor: "pointer",
                        }}
                    >
                        Ja
                    </button>
                    <button
                        onClick={() => {
                            onCancel();
                            props.closeToast?.(); // Sluit de melding
                        }}
                        style={{
                            backgroundColor: "red",
                            color: "white",
                            border: "none",
                            padding: "5px",
                            cursor: "pointer",
                        }}
                    >
                        Nee
                    </button>
                </div>
            </div>
        ),
        {
            autoClose: false,
            closeOnClick: false,
            position: "top-center",
        }
    );
}
