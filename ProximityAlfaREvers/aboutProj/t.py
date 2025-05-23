from pathlib import Path

# --- настроить под себя -------------------------------------------------
known_names = {"t.py"}   # файлы, которые трогать нельзя
known_prefixes = {"l"}                         # уже правильные имена "p1.png"…
target_prefix = "p"                            # новый префикс
target_ext    = ".png"                         # конечное расширение
# ------------------------------------------------------------------------

def is_unknown(file: Path) -> bool:
    """Возвращает True, если файл нужно переименовать."""
    name = file.name.lower()

    # 1. Явно известные имена
    if name in known_names:
        return False

    # 2. Уже имеет правильный префикс p<N>.png ?
    if any(name.startswith(pref) and name.endswith(target_ext) for pref in known_prefixes):
        return False

    return True

def rename_unknown_files():
    cwd = Path.cwd()
    unknown_files = [p for p in cwd.iterdir() if p.is_file() and is_unknown(p)]

    # сортируем для стабильного порядка
    unknown_files.sort(key=lambda p: p.name.lower())

    # ищем следующий свободный номер
    existing_numbers = {
        int(p.stem[len(target_prefix):])
        for p in cwd.glob(f"{target_prefix}[0-9]*{target_ext}")
        if p.stem[len(target_prefix):].isdigit()
    }
    next_num = max(existing_numbers, default=0) + 1

    # переименовываем
    for file in unknown_files:
        while (cwd / f"{target_prefix}{next_num}{target_ext}").exists():
            next_num += 1                      # пропускаем занятые номера

        new_name = f"{target_prefix}{next_num}{target_ext}"
        print(f"{file.name}  →  {new_name}")
        file.rename(cwd / new_name)
        next_num += 1

if __name__ == "__main__":
    rename_unknown_files()
